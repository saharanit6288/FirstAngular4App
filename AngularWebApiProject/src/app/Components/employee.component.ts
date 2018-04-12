import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { EmployeeService } from '../Services/employee.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IEmployee } from '../Models/employees';
import { Globals } from '../Shared/globals';
import { Observable } from 'rxjs/Rx';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
    selector: 'emp-comp',
    styles: [`
        .search-results {
          height: 100px;
          overflow-y: scroll;
        }
      `],
    templateUrl: 'app/Htmls/employees.html'
})

export class EmployeeComponent implements OnInit {
    Title: string = "List of Employees";
    employees: IEmployee[];
    employee: IEmployee;
    managers: any[];
    msg: string;
    term: string = "";
    isLoading: boolean = false;
    isFormOpen: boolean = false;
    isListOpen: boolean = true;
    isTerm: boolean = false;
    isScrollable: boolean = true;
    rFrm: FormGroup;
    formTitle: string = "";
    btnTitle: string = "";
    scrollDistance = 2;
    throttle = 1000;
    page = this.globals.page;
    pagesize = this.globals.pageSize;
    imageSrc: SafeUrl;

    @ViewChild('fileInput') fileInput: ElementRef;

    constructor(
        private fb: FormBuilder,
        private _employeeService: EmployeeService,
        private globals: Globals,
        private sanitizer: DomSanitizer) { }

    ngOnInit(): void {

        this.rFrm = this.fb.group({
            ID: [''],
            EmployeeName: ['', Validators.required],
            Designation: [''],
            ManagerID: [''],
            ContactNo: [''],
            EMailId: ['', Validators.email],
            SkillSets: [''],
            CreatedOn: [''],
            CreatedBy: [''],
            UpdatedOn: [''],
            UpdatedBy: [''],
            ImagePath: [''],
            ImageName: [''],
            ImgFile: null
        });

        this.LoadData();
        this.GetManagers();

    }

    onFileChange(event: any) {
        let reader = new FileReader();
        if (event.target.files && event.target.files.length > 0) {
            let file = event.target.files[0];
            console.log(file);
            reader.readAsDataURL(file);
            reader.onload = ((e) => {
                this.imageSrc = e.target['result'];
                this.rFrm.get('ImageName').setValue(file.name);
                this.rFrm.get('ImgFile').setValue(reader.result.split(',')[1]);
            });
        }
    }

    ClearSearchTerm(): void {
        this.term = "";
        this.imageSrc = "";
        this.isTerm = false;
        this.isScrollable = true;
        this.page = this.globals.page;
        this.pagesize = this.globals.pageSize;
        this.LoadData();
    }

    SearchData(): void {
        this.isLoading = true;
        this.isTerm = true;
        this.isScrollable = true;
        this.page = this.globals.page;
        this.pagesize = this.globals.pageSize;
        this._employeeService.get(encodeURIComponent(this.term), this.page, this.pagesize)
            .subscribe(data => {
                this.employees = data;
                this.isLoading = false;
            },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });

    }

    LoadData(): void {
        console.log(this.pagesize + '-' + this.page);
        this.isLoading = true;
        this._employeeService.get(encodeURIComponent(this.term), this.page, this.pagesize)
            .subscribe(emps => { this.employees = emps; this.isLoading = false; },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });

    }

    onScroll(): void {
        if (this.isScrollable) {
            this.isLoading = true;
            this.page++;
            this._employeeService.get(encodeURIComponent(this.term), this.page, this.pagesize)
                .subscribe((emps) => {
                    if (emps.length <= 0) {
                        this.isScrollable = false;
                    }
                    console.log(emps);
                    for (let i = 0; i < emps.length; i++) {
                        this.employees.push(emps[i]);
                    }
                    this.isLoading = false;
                },
                error => {
                    this.msg = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
    }

    GetManagers(): void {
        this._employeeService.getManagers()
            .subscribe(mngrs => this.managers = mngrs);

    }

    addData() {
        this.isFormOpen = true;
        this.isListOpen = false;
        this.formTitle = "Add New Employee";
        this.btnTitle = "Submit";
        this.rFrm.reset();
    }

    editData(id: number) {
        this.isFormOpen = true;
        this.isListOpen = false;
        this.formTitle = "Edit Employee";
        this.btnTitle = "Update";
        this.isLoading = true;
        this._employeeService.getById(id)
            .subscribe(emp => {
                this.employee = emp;
                this.imageSrc = this.sanitizer.bypassSecurityTrustUrl(emp.ImagePath);
                this.rFrm.patchValue(this.employee);
                console.log(this.employee);
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
            this._employeeService.delete(id)
                .subscribe(emps => {
                    this.employees = emps;
                    this.msg = "Data successfully deleted.";
                    this.isLoading = false;
                    this.term = "";
                    this.imageSrc = "";
                    this.isTerm = false;
                    this.isScrollable = true;
                    this.page = this.globals.page;
                    this.pagesize = this.globals.pageSize;
                    this.rFrm.reset();
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
        this.term = "";
        this.imageSrc = "";
        this.isTerm = false;
        this.isScrollable = true;
        this.isFormOpen = false;
        this.isListOpen = true;
        this.rFrm.reset();
    }

    onSubmit(formData: any) {
        this.isLoading = true;
        if (formData._value.ID == null) {
            console.log(formData._value);
            this._employeeService.post(formData._value)
                .subscribe(emps => {
                    this.employees = emps;
                    this.msg = "Data successfully added.";
                    this.isLoading = false;
                    this.isFormOpen = false;
                    this.isListOpen = true;
                    this.term = "";
                    this.imageSrc = "";
                    this.isTerm = false;
                    this.isScrollable = true;
                    this.page = this.globals.page;
                    this.pagesize = this.globals.pageSize;
                    this.rFrm.reset();
                },
                error => {
                    this.formTitle = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
        else {
            console.log(formData._value);
            this._employeeService.put(formData._value.ID, formData._value)
                .subscribe(emps => {
                    this.employees = emps;
                    this.msg = "Data successfully updated.";
                    this.isLoading = false;
                    this.isFormOpen = false;
                    this.isListOpen = true;
                    this.term = "";
                    this.imageSrc = "";
                    this.isTerm = false;
                    this.isScrollable = true;
                    this.page = this.globals.page;
                    this.pagesize = this.globals.pageSize;
                    this.rFrm.reset();
                },
                error => {
                    this.formTitle = 'Error: ' + <any>error;
                    this.isLoading = false;
                });
        }
    }

    
}  