import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IProject } from '../Models/project';
import { Observable } from 'rxjs/Rx';
import { Globals } from '../Shared/globals';

@Component({
    selector: 'proj-comp',
    styles: [`
        .search-results {
          height: 100px;
          overflow-y: scroll;
        }
      `],
    templateUrl: 'app/Htmls/projects.html'
})

export class ProjectComponent implements OnInit {
    Title: string = "List of Projects";
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
        private _projectService: ProjectService,
        private globals: Globals) { }

    ngOnInit(): void {

        this.rFrm = this.fb.group({
            ID: [''],
            ProjectName: ['', Validators.required],
            StartDate: [''],
            EndDate: [''],
            ClientName: [''],
            CreatedOn: [''],
            CreatedBy: [''],
            UpdatedOn: [''],
            UpdatedBy: [''],
        });


        this.LoadData();

    }
    
    LoadData(): void {
        this.isLoading = true;
        this._projectService.getByPaging(this.page, this.pagesize)
            .subscribe(data => { this.projects = data; this.isLoading = false; },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });

    }

    onScroll(): void {
        this.isLoading = true;
        this.page++;
        this._projectService.getByPaging(this.page, this.pagesize)
            .subscribe((data) => {
                for (let i = 0; i < data.length; i++) {
                    this.projects.push(data[i]);
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
        this.rFrm.reset();
    }

    editData(id: number) {
        this.isFormOpen = true;
        this.isListOpen = false;
        this.formTitle = "Edit Details";
        this.btnTitle = "Update";
        this.isLoading = true;
        this._projectService.getById(id)
            .subscribe(data => {
                this.project = data;
                this.project.StartDate = this.globals.convertToClientDate(data.StartDate);
                this.project.EndDate = this.globals.convertToClientDate(data.EndDate);
                //console.log(this.project);
                this.rFrm.patchValue(this.project);
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
            this._projectService.delete(id)
                .subscribe(data => {
                    this.projects = data;
                    this.msg = "Data successfully deleted.";
                    this.isLoading = false;
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
        this.rFrm.reset();
    }

    onSubmit(formData: any) {
        this.isLoading = true;
        if (formData._value.ID == null) {
            //console.log(formData._value);
            this._projectService.post(formData._value)
                .subscribe(data => {
                    this.projects = data;
                    this.msg = "Data successfully added.";
                    this.isLoading = false;
                    this.isFormOpen = false;
                    this.isListOpen = true;
                },
                error => {
                    this.formTitle = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
        else {
            //console.log(formData._value);
            this._projectService.put(formData._value.ID, formData._value)
                .subscribe(data => {
                    this.projects = data;
                    this.msg = "Data successfully updated.";
                    this.isLoading = false;
                    this.isFormOpen = false;
                    this.isListOpen = true;
                },
                error => {
                    this.formTitle = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
    }

    
}  