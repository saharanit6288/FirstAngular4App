import { Component, OnInit } from '@angular/core';
import { UserStoryService } from '../Services/userstory.service';
import { ProjectService } from '../Services/project.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUserStory } from '../Models/userstory';
import { IProject } from '../Models/project';
import { Globals } from '../Shared/globals';
import { Observable } from 'rxjs/Rx';
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";

@Component({
    selector: 'usrstry-comp',
    styles: [`
        .search-results {
          height: 100px;
          overflow-y: scroll;
        }
      `],
    templateUrl: 'app/Htmls/userStories.html'
})

export class UserStoryComponent implements OnInit {
    Title: string = "List of User Stories";
    userStories: IUserStory[];
    userStory: IUserStory;
    projects: IProject[];
    project: IProject;
    msg: string;
    isLoading: boolean = false;
    isFormOpen: boolean = false;
    isListOpen: boolean = true;
    rFrm: FormGroup;
    formTitle: string = "";
    btnTitle: string = "";
    pathUrl: string;
    scrollDistance = 2;
    throttle = 1000;
    page = this.globals.page;
    pagesize = this.globals.pageSize;

    constructor(private fb: FormBuilder,
        private _userStoryService: UserStoryService,
        private _projectService: ProjectService,
        private globals: Globals) { }

    ngOnInit(): void {

        this.rFrm = this.fb.group({
            ID: [''],
            Title: ['', Validators.required],
            Story: [''],
            ProjectID: ['', Validators.required],
            CreatedOn: [''],
            CreatedBy: [''],
            UpdatedOn: [''],
            UpdatedBy: [''],
        });

        this.LoadData();
        this.GetProjects();
    }

    LoadData(): void {
        this.isLoading = true;
        this._userStoryService.getByPaging(this.page, this.pagesize)
            .subscribe(data => { this.userStories = data; this.isLoading = false; },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });

    }

    GetProjects(): void {
        this._projectService.getProjectsDDL()
            .subscribe(data => this.projects = data);

    }

    SetProjectID(value:any) {
        this.rFrm.patchValue({ ProjectID: value.ID});
    }

    onScroll(): void {
        this.isLoading = true;
        this.page++;
        this._userStoryService.getByPaging(this.page, this.pagesize)
            .subscribe((data) => {
                console.log(data);
                for (let i = 0; i < data.length; i++) {
                    this.userStories.push(data[i]);
                }
                this.isLoading = false;
            },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });
    }

    addData() {
        this.isFormOpen = true;
        this.isListOpen = false;
        this.formTitle = "Add New Data";
        this.btnTitle = "Submit";
        this.project = null;
        this.rFrm.reset();
    }

    editData(id: number) {
        this.isFormOpen = true;
        this.isListOpen = false;
        this.formTitle = "Edit Details";
        this.btnTitle = "Update";
        this.isLoading = true;
        this._userStoryService.getById(id)
            .subscribe(data => {
                this.userStory = data;
                this.project = data.Project;
                this.rFrm.patchValue(this.userStory);
                this.isLoading = false;
            },
            error => {
                this.formTitle = 'Error: ' + <any>error;
                this.isLoading = false;
            });
    }

    deleteData(id: number) {
        this.isLoading = true;
        if (confirm("Do you want to delete?")) {
            this._userStoryService.delete(id)
                .subscribe(data => {
                    this.userStories = data;
                    this.msg = "Data successfully deleted.";
                    this.isLoading = false;
                    this.page = this.globals.page;
                    this.pagesize = this.globals.pageSize;
                },
                error => {
                    this.msg = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
        else {
            this.isLoading = false;
        }
    }

    cancel() {
        this.isFormOpen = false;
        this.isListOpen = true;
        this.project = null;
        this.rFrm.reset();
    }

    onSubmit(formData: any) {
        this.isLoading = true;
        if (formData._value.ID == null) {
            this._userStoryService.post(formData._value)
                .subscribe(data => {
                    this.userStories = data;
                    this.msg = "Data successfully added.";
                    this.isLoading = false;
                    this.isFormOpen = false;
                    this.isListOpen = true;
                    this.page = this.globals.page;
                    this.pagesize = this.globals.pageSize;
                },
                error => {
                    this.formTitle = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
        else {
            this._userStoryService.put(formData._value.ID, formData._value)
                .subscribe(data => {
                    this.userStories = data;
                    this.msg = "Data successfully updated.";
                    this.isLoading = false;
                    this.isFormOpen = false;
                    this.isListOpen = true;
                    this.page = this.globals.page;
                    this.pagesize = this.globals.pageSize;
                },
                error => {
                    this.formTitle = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
    }

    
}  