import Swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Tournament } from 'src/app/shared/models/tournament.model';
import { TournamentService } from 'src/app/shared/services/tournament.service';

@Component({
  selector: 'app-add-tournament',
  templateUrl: './add-tournament.component.html',
  styleUrls: ['./add-tournament.component.sass']
})
export class AddTournamentComponent implements OnInit {

  tournament: Tournament = {};
  isUpdate = false;
  constructor(private tournamentService: TournamentService,  private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    if (this.route.snapshot.params.id){
      this.isUpdate = true;
      this.tournamentService.get(this.route.snapshot.params.id).subscribe(t => this.tournament = t);
    }
  }

  saveChanges(form: NgForm): void {
    if (this.isUpdate){
      this.tournamentService.update(this.route.snapshot.params.id, form.value).subscribe(() => {
        this.handleSuccess();
      });
    }
    else{
      this.tournamentService.create(form.value).subscribe(() => {
        this.handleSuccess();
      });
    }
  }

  cancel(): void {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  handleSuccess(): void{
    Swal.fire({
      icon: 'success',
      title: 'Tournament successfully saved.',
      showConfirmButton: false,
      timer: 1500
    });
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
