import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StudentListComponent } from './components/student-list/student-list.component';
import { RequestListComponent } from './components/request-list/request-list.component';
import { StudentEditorComponent } from './components/student-editor/student-editor.component';
import { LoginComponent } from './components/login/login.component';
import { AuthorizationCheck } from './shared/authorizationCheck';

const routes: Routes = [
  {path: 'student-list', component: StudentListComponent, pathMatch: 'full', canActivate: [AuthorizationCheck]},
  {path: 'request', component: RequestListComponent, pathMatch: 'full', canActivate: [AuthorizationCheck]},
  {path: 'student', component: StudentEditorComponent, pathMatch: 'full', canActivate: [AuthorizationCheck]},
  {path: 'student/:id', component: StudentEditorComponent, pathMatch: 'full', canActivate: [AuthorizationCheck]},
  {path: '', component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
