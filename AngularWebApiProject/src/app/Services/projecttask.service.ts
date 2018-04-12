import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { AuthenticationService } from '../Services/authentication.service';
import { Globals } from '../Shared/globals';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class ProjectTaskService {
    token: any;
    pathUrl: string;

    constructor(private auth: AuthenticationService, private _http: Http, private globals: Globals) {
        this.auth.getUserAuth().subscribe(data => { this.token = data.utoken });
        this.pathUrl = globals.BASE_USER_ENDPOINT + 'api/projecttasks/';
    }

    get(): Observable<any> {
        let headers = new Headers();
        let url = this.pathUrl;
        headers.append('Authorization', 'Bearer ' + this.token);
        let options = new RequestOptions({ headers: headers });
        return this._http.get(url, options)
            .map((response: Response) => <any>response.json())
            //.do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    getByPaging(page: number, pagesize: number): Observable<any> {
        let headers = new Headers();
        let url = this.pathUrl + 'GetProjectTasksByPaging?page=' + page + '&pagesize=' + pagesize;
        headers.append('Authorization', 'Bearer ' + this.token);
        let options = new RequestOptions({ headers: headers });
        return this._http.get(url, options)
            .map((response: Response) => <any>response.json())
            //.do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    //getManagers(): Observable<any> {
    //    let headers = new Headers();
    //    let url = this.pathUrl + 'GetManagers';
    //    headers.append('Authorization', 'Bearer ' + this.token);
    //    let options = new RequestOptions({ headers: headers });
    //    return this._http.get(url, options)
    //        .map((response: Response) => <any>response.json())
    //        //.do(data => console.log("All: " + JSON.stringify(data)))
    //        .catch(this.handleError);
    //}

    getById(id: number): Observable<any> {
        let headers = new Headers();
        let url = this.pathUrl + id;
        headers.append('Authorization', 'Bearer ' + this.token);
        let options = new RequestOptions({ headers: headers });
        return this._http.get(url, options)
            .map((response: Response) => <any>response.json())
            //.do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    post(model: any): Observable<any> {
        let body = JSON.stringify(model);
        let headers = new Headers();
        let url = this.pathUrl;
        headers.append('Authorization', 'Bearer ' + this.token);
        headers.append('Content-Type', 'application/json');
        let options = new RequestOptions({ headers: headers });
        return this._http.post(url, body, options)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    put(id: number, model: any): Observable<any> {
        let body = JSON.stringify(model);
        let headers = new Headers();
        let url = this.pathUrl + id;
        headers.append('Authorization', 'Bearer ' + this.token);
        headers.append('Content-Type', 'application/json');
        let options = new RequestOptions({ headers: headers });
        return this._http.put(url, body, options)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    delete(id: number): Observable<any> {
        let headers = new Headers();
        let url = this.pathUrl + id;
        headers.append('Authorization', 'Bearer ' + this.token);
        headers.append('Content-Type', 'application/json');
        let options = new RequestOptions({ headers: headers });
        return this._http.delete(url, options)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        //console.error(error);
        let msg = error.json();
        //console.error(msg);
        let errorMsg: any;
        if (msg.hasOwnProperty('error_description'))
            errorMsg = error.json().error_description;
        if (msg.hasOwnProperty('Message'))
            errorMsg = error.json().Message;
        if (msg.hasOwnProperty('ModelState'))
            errorMsg = error.json().Message;
        console.log(errorMsg);
        return Observable.throw(errorMsg || 'Server error');
    }
}