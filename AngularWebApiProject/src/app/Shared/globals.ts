import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';

@Injectable()
export class Globals {
    public BASE_USER_ENDPOINT = 'http://localhost:63428/';
    //public BASE_USER_ENDPOINT = 'http://saharanit8826hot-001-site1.itempurl.com/';
    public page = 1;
    public pageSize = 10;

    constructor(private datePipe: DatePipe) { }

    convertToClientDate(inputFormat: any): string {
        let d = new Date(inputFormat);
        return [d.getFullYear(), this.pad(d.getMonth() + 1), this.pad(d.getDate())].join('-');
    }

    pad(s: any): any { return (s < 10) ? '0' + s : s; }

    transformDate(date: any): string {
        return this.datePipe.transform(date, 'yyyy-MM-dd'); //whatever format you need. 
    }

}