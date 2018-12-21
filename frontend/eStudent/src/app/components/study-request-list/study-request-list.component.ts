import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { UserCourse } from 'src/app/models/usercourse.model';
import { CourseService } from 'src/app/services/course.service';

@Component({
  selector: 'app-study-request-list',
  templateUrl: './study-request-list.component.html',
  styleUrls: ['./study-request-list.component.scss']
})
export class StudyRequestListComponent implements OnInit {

  displayedColumns: string[] = ['oib', 'firstName', 'lastName', 'courseName','actions'];
  dataSource:  MatTableDataSource<UserCourse>;

  @ViewChild(MatPaginator) paginator: MatPaginator; 
  @ViewChild(MatSort) sort: MatSort;

  constructor( private courseService: CourseService) { }

  ngOnInit() {
    this.refreshTable();
  }

  refreshTable(){
    this.courseService.getAllUnacceptedRequests().subscribe(r => {
      console.log(r);
      this.dataSource = new MatTableDataSource(r);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  onAcceptRequest(studentCourse: UserCourse){
    this.courseService.acceptRequest(studentCourse.id).subscribe(r => {
      this.refreshTable();
    });
  }

  onDeleteRequest(studentCourse: UserCourse){
    console.log(studentCourse);
    this.courseService.deleteRequest(studentCourse.id).subscribe(r => {
      this.refreshTable();
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
