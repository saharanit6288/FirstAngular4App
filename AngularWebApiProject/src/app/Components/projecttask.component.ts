import { Component, OnInit } from '@angular/core';
import { ProjectTaskService } from '../Services/projecttask.service';
import { EmployeeService } from '../Services/employee.service';
import { UserStoryService } from '../Services/userstory.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IProjectTask } from '../Models/projecttask';
import { Globals } from '../Shared/globals';
import { Observable } from 'rxjs/Rx';


@Component({
    selector: 'projtask-comp',
    styles: [`
        .search-results {
          height: 100px;
          overflow-y: scroll;
        }
      `],
    templateUrl: 'app/Htmls/projectTasks.html'
})

export class ProjectTaskComponent implements OnInit {
    Title: string = "List of Project Tasks";
    projectTasks: IProjectTask[];
    projectTask: IProjectTask;
    employees: any[];
    userStories: any[];
    msg: string;
    isLoading: boolean = false;
    isFormOpen: boolean = false;
    isListOpen: boolean = true;
    rFrm: FormGroup;
    formTitle: string = "";
    btnTitle: string = "";
    scrollDistance = 2;
    throttle = 1000;
    page = this.globals.page;
    pagesize = this.globals.pageSize;
	employeeAutoCompleteUrl: string;

    constructor(private fb: FormBuilder,
        private _projectTaskService: ProjectTaskService,
        private _employeeService: EmployeeService,
        private _userStoryService: UserStoryService,
        private globals: Globals) { }

    ngOnInit(): void {

        this.rFrm = this.fb.group({
            ID: [''],
            EmployeeID: ['', Validators.required],
            TaskStartDate: [''],
            TaskEndDate: [''],
            TaskCompletion: [''],
            UserStoryID: ['', Validators.required],
            CreatedOn: [''],
            CreatedBy: [''],
            UpdatedOn: [''],
            UpdatedBy: [''],
        });

        this.LoadData();
        this.GetEmployees();
        this.GetUserStories();
		this.employeeAutoCompleteUrl=this._employeeService.getEmployeeAutoCompleteUrl();
    }

    GetEmployees(): void {
        this._employeeService.getEmployeesDDL()
            .subscribe(data => this.employees = data);

    }

    GetUserStories(): void {
        this._userStoryService.getUserStoriesDDL()
            .subscribe(data => this.userStories = data);

    }
	
    LoadData(): void {
        this.isLoading = true;
        this._projectTaskService.getByPaging(this.page, this.pagesize)
            .subscribe(data => { this.projectTasks = data; this.isLoading = false; },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });

    }

    onScroll(): void {
        this.isLoading = true;
        this.page++;
        this._projectTaskService.getByPaging(this.page, this.pagesize)
            .subscribe((data) => {
                console.log(data);
                for (let i = 0; i < data.length; i++) {
                    this.projectTasks.push(data[i]);
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
        this._projectTaskService.getById(id)
            .subscribe(data => {
                this.projectTask = data;
                this.projectTask.TaskStartDate = this.globals.convertToClientDate(data.TaskStartDate);
                this.projectTask.TaskEndDate = this.globals.convertToClientDate(data.TaskEndDate);
                this.rFrm.patchValue(this.projectTask);
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
            this._projectTaskService.delete(id)
                .subscribe(data => {
                    this.projectTasks = data;
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
        this.rFrm.reset();
    }

    onSubmit(formData: any) {
        this.isLoading = true;
        if (formData._value.ID == null) {
            this._projectTaskService.post(formData._value)
                .subscribe(data => {
                    this.projectTasks = data;
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
            this._projectTaskService.put(formData._value.ID, formData._value)
                .subscribe(data => {
                    this.projectTasks = data;
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