import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MatTableDataSource, MatPaginator, MatSort, MatDialogConfig, MatDialog } from '@angular/material';
import { Subject } from 'src/app/models/subject.model';
import { SubjectService } from 'src/app/services/subject.service';
import { ActivatedRoute } from '@angular/router';
import { SubjectEditorComponent } from '../subject-editor/subject-editor.component';

@Component({
  selector: 'app-course-subject-editor',
  templateUrl: './course-subject-editor.component.html',
  styleUrls: ['./course-subject-editor.component.scss']
})
export class CourseSubjectEditorComponent implements OnInit {

  id: number;

  displayedColumns: string[] = ['name', 'ectsPoints', 'actions'];
  dataSource:  MatTableDataSource<Subject>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort; 

  constructor(private dialogRef: MatDialogRef<CourseSubjectEditorComponent>, private subjectService: SubjectService, private route: ActivatedRoute, private dialog: MatDialog) {
    this.id = subjectService.courseId;
  }

  ngOnInit() {
    this.refreshTable();
  }

  refreshTable(){
    this.subjectService.getSubjectsForSelect(this.id).subscribe(s => {
      this.dataSource = new MatTableDataSource(s);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  atAddSubjectToCourse(subject: Subject){
    this.subjectService.addSubjectToCourse(subject.id, this.id).subscribe();
    this.dataSource.data = this.dataSource.data.filter(s => s != subject);
  }

  close(){
    this.dialogRef.close();
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openDialog(){
    //this.subjectService.courseId = this.id;
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(SubjectEditorComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data === true) {
          this.refreshTable(); 
        }      
      }
    ); 
    
  }

}
