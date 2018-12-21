import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit { 

  showFiller = false;
  
  constructor(public auth: AuthenticationService) { 
    
  }

  ngOnInit() {
  }

}
