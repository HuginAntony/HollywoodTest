import { EventDetailsService } from 'src/app/shared/services/event.details.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from 'src/app/shared/services/event.service';
import { Event } from 'src/app/shared/models/event.model';
import { Observable } from 'rxjs';
import { EventDetail } from 'src/app/shared/models/event-detail.model';
import Swal from 'sweetalert2';
import { Roles } from 'src/app/shared/enums/roles.enum';
import { User } from 'src/app/shared/models/user.model';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-view-event',
  templateUrl: './view-event.component.html',
  styleUrls: ['./view-event.component.scss']
})
export class ViewEventComponent implements OnInit {

  event: Event;
  eventDetails$: Observable<EventDetail[]>;
  eventDetailsToDelete: Array<number> = [];
  user$: Observable<User>;
  roles = Roles;

  constructor(private eventService: EventService, private eventDetailsService: EventDetailsService,
              private authService: AuthService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.eventService.get(this.route.snapshot.params.id).subscribe(e => this.event = e);
    this.eventDetails$ = this.eventService.getEventDetailsByEvent(this.route.snapshot.params.id);
    this.user$ = this.authService.user$;
  }

  deleteEvents(): void {
    this.eventDetailsService.deleteMany(this.eventDetailsToDelete).subscribe(() => {
      this.handleSuccess('All selected event details were deleted.');
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
      this.eventDetailsToDelete.push(eventId);
    }
    else {
      const index = this.eventDetailsToDelete.indexOf(eventId);
      this.eventDetailsToDelete.splice(index, 1);
    }
  }

  selectAll(isChecked: boolean): void {
    const eventCheckBoxes = document.querySelectorAll('.event-checkbox');
    eventCheckBoxes.forEach(ele => {
      const input = ele as  HTMLInputElement;
      input.checked = isChecked;

      if (isChecked){
        this.eventDetailsToDelete.push(Number(input.name));
      }
      else{
        const index = this.eventDetailsToDelete.indexOf(Number(input.name));
        this.eventDetailsToDelete.splice(index, 1);
      }
    });
  }
}
