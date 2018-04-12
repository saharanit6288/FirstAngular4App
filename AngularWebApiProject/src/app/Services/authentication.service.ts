import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Globals } from '../Shared/globals';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class AuthenticationService {
    // change loggedIn to a subject
    public auth_status: Subject<any> = new BehaviorSubject<any>(null);
    pathUrl: string;

    constructor(private _http: Http, private globals: Globals) {
        this.pathUrl = this.globals.BASE_USER_ENDPOINT;
    }
    
    getCurrentUser(): Observable<any> {
        let storage = localStorage.getItem('currentUser');

        if (storage) {
            return JSON.parse(storage);
        } else {
            return null;
        }
    };

    getUserAuth() {
        if (this.getCurrentUser() != null) {
            let user = JSON.parse(localStorage.getItem('currentUser'));
            this.auth_status.next({ status: true, uname: user.userName, utoken: user.access_token });
            console.log(this.auth_status);
        } else {
            this.auth_status.next({ status: false, uname: '', utoken: '' });
            console.log(this.auth_status);
        }
        return this.auth_status;
    }
    

    login(model: any): Observable<any> {
        let body = "userName=" + encodeURIComponent(model.username) + "&password=" + encodeURIComponent(model.password) + "&grant_type=password";
        let url = this.pathUrl + 'TOKEN';
        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(url, body, options)
            .map((response: Response) => {
                let user = response.json();
                //console.log(JSON.stringify(user));
                if (user) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.auth_status.next({ status: true, uname: user.userName, utoken: user.access_token });
                    //console.log('logged in');
                    //console.log('login:' + this.auth_status);
                }

                return user;
            }
            )
            .catch(this.handleError);
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        localStorage.clear();
        this.auth_status.next({ status: false, uname: '', utoken: '' });
        //console.log('logged out');
        //console.log('logout:' + this.auth_status);
    }

    private handleError(error: Response) {
        console.error(error);
        let msg = error.json();
        console.error(msg);
        let errorMsg: any;
        if (msg.hasOwnProperty('error_description'))
            errorMsg = error.json().error_description;
        if (msg.hasOwnProperty('ModelState'))
            errorMsg = error.json().Message;
        console.log(errorMsg);
        return Observable.throw(errorMsg || 'Server error' );
    }
    
}