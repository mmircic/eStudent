import { Component, OnInit, ViewChild, SimpleChanges } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';
import { MatPaginator, MatTableDataSource, MatSort, MatDialogConfig, MatDialog} from '@angular/material';
import { AuthenticationService, TOKEN_NAME } from 'src/app/services/authentication.service';
import * as jwt_decode from "jwt-decode";
import { Router } from '@angular/router';
import { StudentEditorComponent } from '../student-editor/student-editor.component';

@Component({
  selector: 'student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent implements OnInit {

  displayedColumns: string[] = ['oib', 'firstName', 'lastName', 'birthDate', 'residence', 'email', 'actions'];
  dataSource:  MatTableDataSource<User>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort; 

  constructor(private userService: UserService, private auth: AuthenticationService, private router: Router, private dialog: MatDialog) {
    // if (this.auth.authenticatedUser.role != 'Administrator') {
    //   this.router.navigate(['/request']);
    // }
  }

  

  ngOnInit() { 

    this.refreshTable();
  }


  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onDeleteUser(student, id: number){
    if (confirm('Jeste li sigurni da želite izbrisati studenta?')) {
      this.userService.deleteUser(id).subscribe();
      this.dataSource.data = this.dataSource.data.filter(s => s != student);
    }
  }

  openDialog(student?) {

    this.userService.User = student;
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(StudentEditorComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {if (data === true) {
       this.refreshTable(); 
      }
      }
    ); 
  }

refreshTable(){
  this.userService.getAllStudents().subscribe(u => {
    this.dataSource = new MatTableDataSource(u);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  });
}

  
  
  
  
}
