import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import {MatToolbarModule, MatIconModule, MatFormFieldModule,
   MatOptionModule, MatSelectModule, MatInputModule, 
   MatGridListModule, MatButtonModule, MatPaginatorModule, 
   MatTableModule,
   MatSortModule,
   MatDialogModule} from '@angular/material';

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
import { StudentEditorComponent } from './components/student-editor/student-editor.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    StudentListComponent,
    RequestListComponent,
    StudentEditorComponent
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
    MatDialogModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: httpInterceptor, multi: true},
    //{provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    AuthorizationCheck,
    AuthenticationService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
