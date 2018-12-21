import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { UserCourse } from 'src/app/models/usercourse.model';
import { CourseService } from 'src/app/services/course.service';
import { UserService } from 'src/app/services/user.service';
import { saveAs } from "file-saver";

@Component({
  selector: 'app-current-students-list',
  templateUrl: './current-students-list.component.html',
  styleUrls: ['./current-students-list.component.scss']
})
export class CurrentStudentsListComponent implements OnInit {

  displayedColumns: string[] = ['oib', 'firstName', 'lastName', 'courseName','actions'];
  dataSource:  MatTableDataSource<UserCourse>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  constructor(private courseService: CourseService, private userService: UserService) { }

  ngOnInit() {
    this.refreshTable();
  } 

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onCreatePdf(studentCourse: UserCourse){
    this.userService.createPdf(studentCourse.id).subscribe(res => {
      saveAs(res, studentCourse.user.firstName + studentCourse.user.lastName);      
    });
  }

  refreshTable(): any {
    this.courseService.getAllCurrentStudents().subscribe(sc => {
      this.dataSource = new MatTableDataSource(sc);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

}
