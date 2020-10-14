import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Tournament } from 'src/app/shared/models/tournament.model';
import { Event } from 'src/app/shared/models/event.model';
import { EventService } from 'src/app/shared/services/event.service';
import { TournamentService } from 'src/app/shared/services/tournament.service';
import Swal from 'sweetalert2';
import { Roles } from 'src/app/shared/enums/roles.enum';
import { User } from 'src/app/shared/models/user.model';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-view-tournament',
  templateUrl: './view-tournament.component.html',
  styleUrls: ['./view-tournament.component.scss']
})
export class ViewTournamentComponent implements OnInit {

  tournament: Tournament;
  events$: Observable<Event[]>;
  eventsToDelete: Array<number> = [];
  user$: Observable<User>;
  roles = Roles;

  constructor(private tournamentService: TournamentService, private eventService: EventService,
              private authService: AuthService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.tournamentService.get(this.route.snapshot.params.id).subscribe(t => this.tournament = t);
    this.events$ = this.tournamentService.getEventByTournament(this.route.snapshot.params.id);
    this.user$ = this.authService.user$;
  }

  deleteEvents(): void {
    this.eventService.deleteMany(this.eventsToDelete).subscribe(() => {
      this.handleSuccess('All selected events were deleted.');
    });
  }

  handleSuccess(message: string): void{
    Swal.fire({
      icon: 'success',
      title: message,
      showConfirmButton: false,
      timer: 1500
    });
  }

  onChange(eventId: number, isChecked: boolean): void {
    if (isChecked) {
      this.eventsToDelete.push(eventId);
    }
    else {
      const index = this.eventsToDelete.indexOf(eventId);
      this.eventsToDelete.splice(index, 1);
    }
  }

  selectAll(isChecked: boolean): void {
    const eventCheckBoxes = document.querySelectorAll('.event-checkbox');
    eventCheckBoxes.forEach(ele => {
      const input = ele as  HTMLInputElement;
      input.checked = isChecked;

      if (isChecked){
        this.eventsToDelete.push(Number(input.name));
      }
      else{
        const index = this.eventsToDelete.indexOf(Number(input.name));
        this.eventsToDelete.splice(index, 1);
      }
    });
  }
}
