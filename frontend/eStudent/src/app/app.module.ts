import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import {MatToolbarModule, MatIconModule, MatFormFieldModule,
   MatOptionModule, MatSelectModule, MatInputModule, 
   MatGridListModule, MatButtonModule, MatPaginatorModule, 
   MatTableModule,
   MatSortModule} from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';
import { AuthorizationCheck } from './shared/authorizationCheck';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { httpInterceptor } from './Interceptor/httpInterceptor.service';
import { ErrorInterceptor } from './Interceptor/errorInterceptor';
import { AuthenticationService } from './services/authentication.service';
import { StudentListComponent } from './components/student-list/student-list.component';
import { UserService } from './services/user.service';
import { RequestListComponent } from './components/request-list/request-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    StudentListComponent,
    RequestListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    MatInputModule, 
    MatGridListModule,
    MatButtonModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot([
      {path: 'student', component: StudentListComponent, pathMatch: 'full', canActivate: [AuthorizationCheck]},
      {path: 'request', component: RequestListComponent, pathMatch: 'full', canActivate: [AuthorizationCheck]},
      {path: '', component: LoginComponent}
    ])
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: httpInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    AuthorizationCheck,
    AuthenticationService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
