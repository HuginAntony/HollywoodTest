import { EventDetailStatusNames } from './../../../shared/enums/eventDetailStatusNames.enum';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventDetail } from 'src/app/shared/models/event-detail.model';
import { Event } from 'src/app/shared/models/event.model';
import { EventDetailsService } from 'src/app/shared/services/event.details.service';
import { EventService } from 'src/app/shared/services/event.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-event-details',
  templateUrl: './add-event-details.component.html',
  styleUrls: ['./add-event-details.component.sass']
})
export class AddEventDetailsComponent implements OnInit {

  eventDetail: EventDetail = {};
  events: Event[];
  isUpdate = false;
  eventStatusName = EventDetailStatusNames;
  statusKeys: number[];

  constructor(private eventService: EventService, private eventDetailService: EventDetailsService,
              private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.statusKeys = Object.keys(this.eventStatusName).filter((f) => !isNaN(Number(f))).map(Number);

    if (this.route.snapshot.params.id){
      this.isUpdate = true;
      this.eventDetailService.get(this.route.snapshot.params.id).subscribe(e => this.eventDetail = e);
    }
    else{
      this.eventDetail = {
        eventId: 0,
        eventDetailStatusId: this.eventStatusName.Active
      };
    }
    this.eventService.getAll().subscribe(e => this.events = e);
  }

  saveChanges(): void {
    if (this.isUpdate){
      this.eventDetailService.update(this.route.snapshot.params.id, this.eventDetail).subscribe(() => {
        this.handleSuccess('Event detail successfully updated.');
      });
    }
    else{
      this.eventDetailService.create(this.eventDetail).subscribe(() => {
        this.handleSuccess('Event detail successfully created.');
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/eventDetail'], { relativeTo: this.route });
  }

  deleteEventDetail(): void {
    this.eventDetailService.delete(this.eventDetail.eventDetailId).subscribe(() => {
      this.handleSuccess('Event detail successfully deleted.');
    });
  }

  handleSuccess(message: string): void{
    Swal.fire({
      icon: 'success',
      title: message,
      showConfirmButton: false,
      timer: 1500
    });
    this.router.navigate(['/eventDetail'], { relativeTo: this.route });
  }
}
