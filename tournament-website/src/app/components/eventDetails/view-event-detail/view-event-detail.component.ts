import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, timer, Observable } from 'rxjs';
import { switchMap, share, takeUntil } from 'rxjs/operators';
import { EventDetailStatusNames } from 'src/app/shared/enums/eventDetailStatusNames.enum';
import { EventDetail } from 'src/app/shared/models/event-detail.model';
import { EventDetailsService } from 'src/app/shared/services/event.details.service';

@Component({
  selector: 'app-view-event-detail',
  templateUrl: './view-event-detail.component.html',
  styleUrls: ['./view-event-detail.component.scss']
})
export class ViewEventDetailComponent implements OnInit, OnDestroy {
  private stopPolling = new Subject();
  private eventDetail$: Observable<EventDetail>;

  eventDetail: EventDetail;
  eventDetailStatus = EventDetailStatusNames;

  constructor(private eventDetailService: EventDetailsService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.eventDetailService.get(this.route.snapshot.params.id).subscribe(e => this.eventDetail = e);

    this.eventDetail$ = timer(1, 5000).pipe(
      switchMap(() => this.eventDetailService.get(this.route.snapshot.params.id)),
      share(),
      takeUntil(this.stopPolling)
    );

    this.eventDetail$.subscribe(e => this.eventDetail.eventDetailOdd = e.eventDetailOdd);
  }

  ngOnDestroy(): void {
    this.stopPolling.next();
  }
}
