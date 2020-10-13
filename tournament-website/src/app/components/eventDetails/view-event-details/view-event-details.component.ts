import { EventDetailStatusNames } from './../../../shared/enums/eventDetailStatusNames.enum';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { EventDetail } from 'src/app/shared/models/event-detail.model';
import { EventDetailsService } from 'src/app/shared/services/event.details.service';

@Component({
  selector: 'app-view-event-details',
  templateUrl: './view-event-details.component.html',
  styleUrls: ['./view-event-details.component.css']
})
export class ViewEventDetailsComponent implements OnInit {

  eventDetails$: Observable<EventDetail[]>;
  eventDetailStatus = EventDetailStatusNames;

  constructor(private eventDetailService: EventDetailsService) { }

  ngOnInit(): void {
    this.eventDetails$ = this.eventDetailService.getAll();
  }

}
