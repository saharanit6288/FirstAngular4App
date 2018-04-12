import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../Services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ILogin } from '../Models/login';
import { Observable } from 'rxjs/Rx';

@Component({
    selector: 'login-comp',
    templateUrl: 'app/Htmls/login.html'
    
})
export class LoginComponent implements OnInit {
    Title: string = "Please Sign In";
    user: ILogin;
    msg: string;
    isLoading: boolean = false;
    rFrm: FormGroup;
    returnUrl: string;
    model: any = {};

    constructor(
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService) { }

    ngOnInit(): void {
        // reset login status
        this.authenticationService.logout();
        
        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

        this.rFrm = this.fb.group({
            Email: ['', Validators.required],
            Password: ['', Validators.required]
        });
    }

    login(formData: any) {
        this.model.username = formData._value.Email;
        this.model.password = formData._value.Password;

        this.isLoading = true;
        this.authenticationService.login(this.model)
            .subscribe(
            data => {
                this.router.navigate([this.returnUrl]);
            },
            error => {
                this.msg = 'Error: ' + <any>error;
                this.isLoading = false;
            });
    }
}  