import Swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginRequest } from 'src/app/models/login-request.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginRequest: LoginRequest = {};
  constructor(private authService: AuthService,  private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
  }

  saveChanges(form: NgForm): void {
      Swal.showLoading();
      this.authService.signIn(form.value).subscribe(() => {
        Swal.fire();
        this.router.navigate(['../home'], { relativeTo: this.route });
      });
  }
}
