import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Globals } from '../Shared/globals';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class AccountService {
    pathUrl: string;

    constructor(private _http: Http, private globals: Globals) {
        this.pathUrl = this.globals.BASE_USER_ENDPOINT + 'api/Account/';
    }

    get(): Observable<any> {
        let url = this.pathUrl;
        return this._http.get(url)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    post(model: any): Observable<any> {
        let body = JSON.stringify(model);
        let url = this.pathUrl + 'Register';
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(url, body, options)
            .map((response: Response) => <any>response)
            .catch(this.handleError);
    }

    put(id: number, model: any): Observable<any> {
        let body = JSON.stringify(model);
        let url = this.pathUrl;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.put(url + id, body, options)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error(error);
        let msg = error.json();
        let errorMsg: any;
        if (msg.hasOwnProperty('error_description')) {
            errorMsg = error.json().error_description;
        }
        if (msg.hasOwnProperty('ModelState')) {
            let modelMsg = msg.ModelState;
            if (modelMsg.hasOwnProperty('model.Password')) {
                errorMsg = error.json().ModelState['model.Password'][0];
            }
            else if (modelMsg.hasOwnProperty('model.ConfirmPassword')) {
                errorMsg = error.json().ModelState['model.ConfirmPassword'][0];
            }
            else if (modelMsg.hasOwnProperty('')) {
                errorMsg = error.json().ModelState[''][0];
            }
        }
        return Observable.throw(errorMsg || 'Server error');
    }
}