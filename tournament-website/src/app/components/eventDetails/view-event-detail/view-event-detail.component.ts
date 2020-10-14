import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, timer, Observable } from 'rxjs';
import { switchMap, share, takeUntil } from 'rxjs/operators';
import { EventDetailStatusNames } from 'src/app/shared/enums/eventDetailStatusNames.enum';
import { Roles } from 'src/app/shared/enums/roles.enum';
import { EventDetail } from 'src/app/shared/models/event-detail.model';
import { Event } from 'src/app/shared/models/event.model';
import { User } from 'src/app/shared/models/user.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { EventDetailsService } from 'src/app/shared/services/event.details.service';
import { EventService } from 'src/app/shared/services/event.service';

@Component({
  selector: 'app-view-event-detail',
  templateUrl: './view-event-detail.component.html',
  styleUrls: ['./view-event-detail.component.scss']
})
export class ViewEventDetailComponent implements OnInit, OnDestroy {
  private stopPolling = new Subject();
  private eventDetail$: Observable<EventDetail>;

  eventDetail: EventDetail;
  event: Event;
  eventDetailStatus = EventDetailStatusNames;
  user$: Observable<User>;
  roles = Roles;

  constructor(private eventDetailService: EventDetailsService, private eventService: EventService,
              private authService: AuthService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.user$ = this.authService.user$;

    this.eventDetailService.get(this.route.snapshot.params.id).subscribe(ed =>
              {
                this.eventDetail = ed;
                this.eventService.get(this.eventDetail.eventId).subscribe(e => this.event = e);
              });

    this.eventDetail$ = timer(1, 5000).pipe(
      switchMap(() => this.eventDetailService.get(this.route.snapshot.params.id)),
      share(),
      takeUntil(this.stopPolling)
    );

    this.eventDetail$.subscribe(e =>
      {
          this.eventDetail.eventDetailOdd = e.eventDetailOdd;
      },
      error => this.stopPolling.next());
  }

  ngOnDestroy(): void {
    this.stopPolling.next();
  }
}
