import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StudentListComponent } from './components/student-list/student-list.component';
import { LoginComponent } from './components/login/login.component';
import { AuthorizationCheck } from './shared/authorizationCheck';
import { CourseListComponent } from './components/course-list/course-list.component';
import { CourseSubjectsListComponent } from './components/course-subjects-list/course-subjects-list.component';
import { AdministratorCheck } from './shared/administrator-check';
import { StudyRequestListComponent } from './components/study-request-list/study-request-list.component';
import { CurrentStudentsListComponent } from './components/current-students-list/current-students-list.component';

const routes: Routes = [
  {path: 'student-list', component: StudentListComponent, pathMatch: 'full', canActivate: [AdministratorCheck]},
  {path: 'course/:courseId/subject', component: CourseSubjectsListComponent, pathMatch: 'full', canActivate: [AuthorizationCheck]},
  {path: 'course-list', component: CourseListComponent, pathMatch: 'full', canActivate: [AuthorizationCheck]},
  {path: 'study-request-list', component: StudyRequestListComponent, pathMatch: 'full', canActivate: [AdministratorCheck]},
  {path: 'current-students-list', component: CurrentStudentsListComponent, pathMatch: 'full', canActivate: [AdministratorCheck]},
  {path: '', component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
