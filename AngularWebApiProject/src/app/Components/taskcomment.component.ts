import { Component, OnInit } from '@angular/core';
import { ProjectTaskService } from '../Services/projecttask.service';
import { ManagerCommentService } from '../Services/managercomment.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IProjectTask } from '../Models/projecttask';
import { IManagerComment } from '../Models/managercomment';
import { Globals } from '../Shared/globals';
import { Observable } from 'rxjs/Rx';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'taskcomment-comp',
    styles: [`
        .search-results {
          height: 100px;
          overflow-y: scroll;
        }
      `],
    templateUrl: 'app/Htmls/taskComments.html'
})

export class TaskCommentComponent implements OnInit {
    TaskTitle: string = "Task Info";
    CommentsTitle: string = "List of Task Comments";
    taskInfo: IProjectTask;
    taskcomments: IManagerComment[];
    taskcomment: IManagerComment;
    msg: string;
    isLoading: boolean = false;
    rFrm: FormGroup;
    formTitle: string = "Add Comments";
    btnTitle: string = "Submit";
    scrollDistance = 2;
    throttle = 1000;
    page = this.globals.page;
    pagesize = this.globals.pageSize;
    taskId: number;
    todayDate: any = new Date();

    constructor(private fb: FormBuilder,
        private _projectTaskService: ProjectTaskService,
        private _managerCommentService: ManagerCommentService,
        private globals: Globals,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.taskId = +this.route.snapshot.params['taskId'];
        this.todayDate = this.globals.transformDate(this.todayDate);
        this.LoadTaskInfo(this.taskId);
        this.LoadCommentsByTask(this.taskId);

        this.rFrm = this.fb.group({
            ID: [''],
            Comments: ['', Validators.required],
            CommentDate: [''],
            ProjectTaskID: [''],
            CreatedOn: [''],
            CreatedBy: [''],
            UpdatedOn: [''],
            UpdatedBy: [''],
        });

        this.rFrm.patchValue({ ProjectTaskID: this.taskId });
    }

    

    LoadTaskInfo(taskID:number): void {
        this.isLoading = true;
        this._projectTaskService.getById(taskID)
            .subscribe(data => {
                console.log(data);
                this.taskInfo = data;
                this.isLoading = false;
            },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });

    }

    LoadCommentsByTask(taskID: number): void {
        this.isLoading = true;
        this._managerCommentService.getCommentsByTask(taskID)
            .subscribe(data => {
                console.log(data);
                this.taskcomments = data;
                this.isLoading = false;
            },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });

    }

    //addData() {
    //    this.isFormOpen = true;
    //    this.isListOpen = false;
    //    this.formTitle = "Add New Data";
    //    this.btnTitle = "Submit";
    //    this.rFrm.reset();
    //}

    editData(id: number) {
        this.formTitle = "Edit Comments";
        this.btnTitle = "Update";
        this.isLoading = true;
        this._managerCommentService.getById(id)
            .subscribe(data => {
                this.taskcomment = data;
                this.rFrm.patchValue(this.taskcomment);
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
            this._managerCommentService.delete(id)
                .subscribe(data => {
                    this.taskcomments = data;
                    this.msg = "Data successfully deleted.";
                    this.isLoading = false;
                    this.cancel();
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
        this.rFrm.reset();
        this.formTitle = "Add Comments";
        this.rFrm.patchValue({ ProjectTaskID: this.taskId });
    }

    onSubmit(formData: any) {
        this.isLoading = true;
        console.log(formData._value);
        if (formData._value.ID == null || formData._value.ID == '') {
            this._managerCommentService.post(formData._value)
                .subscribe(data => {
                    this.taskcomments = data;
                    this.msg = "Data successfully added.";
                    this.isLoading = false;
                    this.cancel();
                },
                error => {
                    this.formTitle = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
        else {
            this._managerCommentService.put(formData._value.ID, formData._value)
                .subscribe(data => {
                    this.taskcomments = data;
                    this.msg = "Data successfully updated.";
                    this.isLoading = false;
                    this.cancel();
                },
                error => {
                    this.formTitle = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
    }


}  