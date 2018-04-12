import { NgModule } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NguiAutoCompleteModule } from '@ngui/auto-complete';
import { HttpModule } from '@angular/http';
import { AuthGuard } from './Guards/auth.guard';
import { routing } from './app.routes';
import { Globals } from './Shared/globals';
import { DatePipe } from '@angular/common';

import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home.component';
import { EmployeeComponent } from './Components/employee.component';
import { LoginComponent } from './Components/login.component';
import { RegisterComponent } from './Components/register.component';
import { ManagerCommentComponent } from './Components/managercomment.component';
import { ProjectTaskComponent } from './Components/projecttask.component';
import { ProjectComponent } from './Components/project.component';
import { UserStoryComponent } from './Components/userstory.component';
import { TaskCommentComponent } from './Components/taskcomment.component';

import { AuthenticationService } from './Services/authentication.service';
import { AccountService } from './Services/account.service';
import { EmployeeService } from './Services/employee.service';
import { ProjectService } from './Services/project.service';
import { ProjectTaskService } from './Services/projecttask.service';
import { UserStoryService } from './Services/userstory.service';
import { ManagerCommentService } from './Services/managercomment.service';


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        routing,
        InfiniteScrollModule,
        NguiAutoCompleteModule
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        EmployeeComponent,
        LoginComponent,
        RegisterComponent,
        ManagerCommentComponent,
        ProjectTaskComponent,
        ProjectComponent,
        UserStoryComponent,
        TaskCommentComponent
    ],
    providers: [
        { provide: APP_BASE_HREF, useValue: '/' },
        Globals,
        AuthGuard,
        DatePipe,
        AccountService,
        AuthenticationService,
        EmployeeService,
        ProjectService,
        ProjectTaskService,
        UserStoryService,
        ManagerCommentService
    ],
    bootstrap: [AppComponent] 
})
export class AppModule { }
