import { Component, OnInit, ViewChild } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { MatTableDataSource, MatPaginator, MatSort, MatDialogConfig, MatDialog } from '@angular/material';
import { Course } from 'src/app/models/course.model';
import { CourseEditorComponent } from '../course-editor/course-editor.component';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.scss']
})
export class CourseListComponent implements OnInit {

  isAdmin: boolean = false;

  displayedColumns: string[] = ['name', 'courseType', 'courseSubjects','actions'];
  dataSource:  MatTableDataSource<Course>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private courseService: CourseService, private dialog: MatDialog, private router: Router, private authService: AuthenticationService) { 
    this.isAdmin = authService.authenticatedUser.role === 'Administrator' ? true : false;
  }

  ngOnInit() {
    this.refreshTable();
  }
  
  refreshTable(){
    this.courseService.getAllCourses().subscribe(c => {
      this.dataSource = new MatTableDataSource(c);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  onDeleteCourse(course: Course, id: number){
    if (confirm('Jeste li sigurni da želite izbrisati studij?')) {
      this.courseService.deleteCourse(id).subscribe();
      this.dataSource.data = this.dataSource.data.filter(c => c != course);
    }
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openDialog(course?){
    this.courseService.Course = course;
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CourseEditorComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {if (data === true) {
       this.refreshTable(); 
      }
      }
    );

  }

  showCourseSubjects(course: any){
    this.router.navigate(['course', course.id, 'subject']);
  }

  onSendRequest(course: Course){
    this.courseService.sendRequest(course.id, this.authService.authenticatedUser.id).subscribe();
  }
}
