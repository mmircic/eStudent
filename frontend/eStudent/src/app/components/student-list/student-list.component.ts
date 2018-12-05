import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';
import { MatPaginator, MatTableDataSource, MatSort} from '@angular/material';
import { AuthenticationService, TOKEN_NAME } from 'src/app/services/authentication.service';
import * as jwt_decode from "jwt-decode";
import { Router } from '@angular/router';

@Component({
  selector: 'student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent implements OnInit {

  displayedColumns: string[] = ['oib', 'firstName', 'lastName', 'birthDate', 'residence', 'email'];
  dataSource:  MatTableDataSource<User>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private userService: UserService, private auth: AuthenticationService, private router: Router) {
    if (this.auth.user.Role !== 'Administrator') {
      this.router.navigate(['/request']);
    }
  }

  ngOnInit() {

    console.log(this.auth.user.Role);

    this.userService.getAllStudents().subscribe(u => {
      this.dataSource = new MatTableDataSource(u);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  
  
  
  
}
