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

  constructor(private eventService: EventService, private eventDetailService: EventDetailsService,
              private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    if (this.route.snapshot.params.id && Number(this.route.snapshot.params.id)){
      this.isUpdate = true;
      this.eventDetailService.get(this.route.snapshot.params.id).subscribe(e => this.eventDetail = e);
    }

    this.eventService.getAll().subscribe(e => this.events = e);
  }

  saveChanges(form: NgForm): void {
    if (this.isUpdate){
      this.eventService.update(this.route.snapshot.params.id, form.value).subscribe(() => {
        this.handleSuccess();
      });
    }
    else{
      this.eventDetailService.create(form.value).subscribe(() => {
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
      title: 'Event detail successfully saved.',
      showConfirmButton: false,
      timer: 1500
    });
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
