import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './Services/authentication.service';

@Component({
  selector: 'my-app',
  templateUrl: '/app/Htmls/layout.html',
})

export class AppComponent implements OnInit {
    loggedIn: any;
    UserName: string = '';

    constructor(private auth: AuthenticationService, private router: Router) {
        this.auth.getUserAuth().subscribe(data => { this.loggedIn = data.status, this.UserName = data.uname });
        //console.log('logged: ' + JSON.stringify(this.loggedIn));
        //console.log('current-userName: ' + this.UserName);
    }

    ngOnInit(): void {
        
    }

    logout() {
        this.auth.logout();
        this.router.navigate(['/Login']);
    }
}
