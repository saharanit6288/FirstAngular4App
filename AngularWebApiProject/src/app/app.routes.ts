import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './Guards/auth.guard';

import { HomeComponent } from './Components/home.component';
import { EmployeeComponent } from './Components/employee.component';
import { LoginComponent } from './Components/login.component';
import { RegisterComponent } from './Components/register.component';
import { ManagerCommentComponent } from './Components/managercomment.component';
import { ProjectTaskComponent } from './Components/projecttask.component';
import { ProjectComponent } from './Components/project.component';
import { UserStoryComponent } from './Components/userstory.component';
import { TaskCommentComponent } from './Components/taskcomment.component';

const appRoutes: Routes = [
    { path: '', redirectTo: 'Home', pathMatch: 'full' },
    { path: 'Home', component: HomeComponent },
    { path: 'Employees', component: EmployeeComponent, canActivate: [AuthGuard] },
    { path: 'Comments', component: ManagerCommentComponent, canActivate: [AuthGuard] },
    { path: 'TaskComments/:taskId', component: TaskCommentComponent, canActivate: [AuthGuard] },
	{ path: 'Tasks', component: ProjectTaskComponent, canActivate: [AuthGuard] },
	{ path: 'Projects', component: ProjectComponent, canActivate: [AuthGuard] },
	{ path: 'UserStories', component: UserStoryComponent, canActivate: [AuthGuard] },
    { path: 'Login', component: LoginComponent },
    { path: 'Register', component: RegisterComponent },
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);