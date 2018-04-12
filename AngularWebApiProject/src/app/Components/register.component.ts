import { Component, OnInit } from '@angular/core';
import { AccountService } from '../Services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IRegister } from '../Models/register';
import { Observable } from 'rxjs/Rx';

@Component({
    selector: 'register-comp',
    templateUrl: 'app/Htmls/register.html',

})

export class RegisterComponent implements OnInit {
    Title: string = "Register Form";
    user: IRegister;
    msg: string;
    isLoading: boolean = false;
    rFrm: FormGroup;
    

    constructor(private fb: FormBuilder, private _accountService: AccountService) { }

    ngOnInit(): void {

        this.rFrm = this.fb.group({
            Email: ['', Validators.required],
            Password: ['', Validators.required],
            ConfirmPassword: ['', Validators.required]
        });

    }
    

    register(formData: any) {
        this.isLoading = true;
        this._accountService.post(formData._value)
            .subscribe(user => {
                //this.user = user;
                this.msg = "Registered successfully.";
                this.isLoading = false;
                this.rFrm.reset();
            },
            error => {
                this.msg = 'Error: ' + <any>error,
                this.isLoading = false;
            });
    }
}  