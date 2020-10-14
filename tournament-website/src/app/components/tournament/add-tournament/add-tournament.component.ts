import { EventService } from 'src/app/shared/services/event.service';
import Swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Tournament } from 'src/app/shared/models/tournament.model';
import { Event } from 'src/app/shared/models/event.model';
import { TournamentService } from 'src/app/shared/services/tournament.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-tournament',
  templateUrl: './add-tournament.component.html',
  styleUrls: ['./add-tournament.component.sass']
})
export class AddTournamentComponent implements OnInit {
  tournament: Tournament = {};
  events$: Observable<Event[]>;
  isUpdate = false;
  eventsToDelete: Array<number> = [];

  constructor(private tournamentService: TournamentService, private eventService: EventService,
              private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    if (this.route.snapshot.params.id){
      this.isUpdate = true;
      this.tournamentService.get(this.route.snapshot.params.id).subscribe(t => this.tournament = t);
    }
    else{
      this.tournament = {};
    }
  }

  saveChanges(form: NgForm): void {
    if (this.isUpdate){
      this.tournamentService.update(this.route.snapshot.params.id, form.value).subscribe(() => {
        this.handleSuccess('Tournament successfully saved.');
      });
    }
    else{
      this.tournamentService.create(form.value).subscribe(() => {
        this.handleSuccess('Tournament successfully created.');
      });
    }
  }

  cancel(): void {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  deleteTournament(): void {
    this.tournamentService.delete(this.tournament.tournamentId).subscribe(() => {
      this.handleSuccess('Tournament successfully delete.');
    });
  }

  handleSuccess(message: string): void{
    Swal.fire({
      icon: 'success',
      title: message,
      showConfirmButton: false,
      timer: 1500
    });
    this.router.navigate(['../'], { relativeTo: this.route });
  }


}
