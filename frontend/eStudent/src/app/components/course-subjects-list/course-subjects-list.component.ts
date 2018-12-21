import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialogConfig, MatDialog } from '@angular/material';
import { Subject } from 'src/app/models/subject.model';
import { SubjectService } from 'src/app/services/subject.service';
import { ActivatedRoute } from '@angular/router';
import { CourseSubjectEditorComponent } from '../course-subject-editor/course-subject-editor.component';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-course-subjects-list',
  templateUrl: './course-subjects-list.component.html',
  styleUrls: ['./course-subjects-list.component.scss']
})
export class CourseSubjectsListComponent implements OnInit {

  id: number;

  isAdmin: boolean;

  displayedColumns: string[] = ['name', 'ectsPoints','actions'];
  dataSource:  MatTableDataSource<Subject>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private subjectService: SubjectService, private route: ActivatedRoute, private dialog: MatDialog, private authService: AuthenticationService) {
    this.isAdmin = authService.authenticatedUser.role === 'Administrator' ? true : false;
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      console.log(params);
      this.id = +params['courseId'];
    });
    this.refreshTable();
  }

  openDialog() {

    this.subjectService.courseId = this.id;
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CourseSubjectEditorComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
       this.refreshTable(); 
      
      }
    ); 
  }

  onDeleteSubject(subject: Subject, subjectId: number){
    if (confirm('Jeste li sigurni da želite izbrisati studenta?')) {
      this.subjectService.deleteSubjectFromCourse(subjectId, this.id).subscribe();
      this.dataSource.data = this.dataSource.data.filter(s => s != subject);
    }
  }

  refreshTable(){
    this.subjectService.getSubjectsByCourse(this.id).subscribe(s => {
      console.log(s);
      this.dataSource = new MatTableDataSource(s);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
