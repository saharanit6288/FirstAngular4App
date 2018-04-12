import { Component } from '@angular/core';
@Component({
    selector: 'home-comp',
    template:
        `<div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">{{Title}}</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
        </div>`
})
export class HomeComponent {
    Title: string = "Dashboard";
}  