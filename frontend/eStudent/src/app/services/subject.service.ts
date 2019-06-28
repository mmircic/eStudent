import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {
  
    
  courseId: number;

  API_BASE_URL = environment.baseUri;
  
  constructor(private http: HttpClient) { }

  getSubjectsByCourse(id: number): any {
    return this.http.get(this.API_BASE_URL + 'subjectcourse/course/' + id);
  }

  getSubjectsForSelect(id: number): any {
    return this.http.get(this.API_BASE_URL + 'subject/course/' + id);
  }

  deleteSubjectFromCourse(subjectId: number, id: number): any {
    return this.http.delete(this.API_BASE_URL + 'subjectcourse/' + subjectId + '/' + id);
  }

  addSubjectToCourse(subjectId: number, id: number): any {
    return this.http.post(this.API_BASE_URL + 'subjectcourse', {'subjectId': subjectId, 'courseId': id}, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  createSubject(subject: any): any {
    return this.http.post(this.API_BASE_URL + 'subject', subject,{
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }
}
