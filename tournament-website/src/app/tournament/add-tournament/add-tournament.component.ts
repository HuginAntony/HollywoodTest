import Swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Tournament } from 'src/app/models/tournament.model';
import { TournamentService } from 'src/app/services/tournament.service';

@Component({
  selector: 'app-add-tournament',
  templateUrl: './add-tournament.component.html',
  styleUrls: ['./add-tournament.component.sass']
})
export class AddTournamentComponent implements OnInit {

  tournament: Tournament = {};
  constructor(private tournamentService: TournamentService,  private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
  }

  saveChanges(form: NgForm): void {
      this.tournamentService.create(form.value).subscribe(() =>{
        Swal.fire({
        icon: 'success',
        title: 'Tournament successfully saved.',
        showConfirmButton: false,
        timer: 1500
        });
        this.router.navigate(['../'], { relativeTo: this.route });
      });
  }

  cancel(): void {
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
