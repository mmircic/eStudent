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
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';
import { AuthorizationCheck } from './shared/authorizationCheck';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { httpInterceptor } from './Interceptor/httpInterceptor.service';
import { ErrorInterceptor } from './Interceptor/errorInterceptor';
import { AuthenticationService } from './services/authentication.service';
import { StudentListComponent } from './components/student-list/student-list.component';
import { UserService } from './services/user.service';
import { StudentEditorComponent } from './components/student-editor/student-editor.component';
import { CourseListComponent } from './components/course-list/course-list.component';
import { CourseService } from './services/course.service';
import { CourseEditorComponent } from './components/course-editor/course-editor.component';
import { CourseTypeService } from './services/course-type.service';
import { CourseSubjectsListComponent } from './components/course-subjects-list/course-subjects-list.component';
import { SubjectService } from './services/subject.service';
import { CourseSubjectEditorComponent } from './components/course-subject-editor/course-subject-editor.component';
import { SubjectEditorComponent } from './components/subject-editor/subject-editor.component';
import { AdministratorCheck } from './shared/administrator-check';
import { StudyRequestListComponent } from './components/study-request-list/study-request-list.component';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { MaterialModule } from './material/material.module';
import { CurrentStudentsListComponent } from './components/current-students-list/current-students-list.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    StudentListComponent,
    StudentEditorComponent,
    CourseListComponent,
    CourseEditorComponent,
    CourseSubjectsListComponent,
    CourseSubjectEditorComponent,
    SubjectEditorComponent,
    StudyRequestListComponent,
    SideNavComponent,
    CurrentStudentsListComponent
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
    FormsModule,
    MaterialModule
    

    
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: httpInterceptor, multi: true},
    //{provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    AuthorizationCheck,
    AdministratorCheck,
    AuthenticationService,
    UserService,
    CourseService,
    CourseTypeService,
    SubjectService
  ],
  entryComponents: [
    StudentEditorComponent,
    CourseEditorComponent,
    CourseSubjectEditorComponent,
    SubjectEditorComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
