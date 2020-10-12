import { User } from './models/event.model';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit{
  title = 'tournament-website';
  user$: Observable<User>;
  constructor(private authService: AuthService){}

ngOnInit(): void{
  this.user$ = this.authService.user$;
}

logout(): void{
    this.authService.signOut();
  }
}
