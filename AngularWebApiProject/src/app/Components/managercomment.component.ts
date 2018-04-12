import { Component, OnInit } from '@angular/core';
import { ManagerCommentService } from '../Services/managercomment.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IManagerComment } from '../Models/managercomment';
import { Globals } from '../Shared/globals';
import { Observable } from 'rxjs/Rx';


@Component({
    selector: 'comment-comp',
    styles: [`
        .search-results {
          height: 100px;
          overflow-y: scroll;
        }
      `],
    templateUrl: 'app/Htmls/managerComments.html'
})

export class ManagerCommentComponent implements OnInit {
    Title: string = "List of Comments";
    managercomments: IManagerComment[];
    managercomment: IManagerComment;
    msg: string;
    term: string;
    isLoading: boolean = false;
    isTerm: boolean = false;
    isListOpen: boolean = true;
    rFrm: FormGroup;
    formTitle: string = "";
    btnTitle: string = "";
    scrollDistance = 2;
    throttle = 1000;
    page = this.globals.page;
    pagesize = this.globals.pageSize;

    constructor(private fb: FormBuilder,
        private _managerCommentService: ManagerCommentService,
        private globals: Globals) { }

    ngOnInit(): void {
        
        this.LoadData();

    }

    ClearSearchTerm(): void {
        this.term = "";
        this.isTerm = false;
        this.page = this.globals.page;
        this.pagesize = this.globals.pageSize;
        this.LoadData();
    }

    SearchData(): void {
        this.isLoading = true;
        this.isTerm = true;
        console.log(encodeURIComponent(this.term));
        this._managerCommentService.search(encodeURIComponent(this.term))
            .subscribe(data => {
                this.managercomments = data;
                this.isLoading = false;
            },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });

    }

    LoadData(): void {
        this.isLoading = true;
        this._managerCommentService.getByPaging(this.page, this.pagesize)
            .subscribe(data => { this.managercomments = data; this.isLoading = false; },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });

    }

    onScroll(): void {
        this.isLoading = true;
        this.page++;
        this._managerCommentService.getByPaging(this.page, this.pagesize)
            .subscribe((data) => {
                console.log(data);
                for (let i = 0; i < data.length; i++) {
                    this.managercomments.push(data[i]);
                }
                this.isLoading = false;
            },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });
    }
    
    
}  