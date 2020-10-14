import { AbstractControl, AsyncValidator, NG_ASYNC_VALIDATORS, ValidationErrors, Validator} from '@angular/forms';
import {Directive, Input} from '@angular/core';
import {Observable, of} from 'rxjs';
import { map } from 'rxjs/operators';
import { TournamentService } from '../services/tournament.service';
import { EventDetailsService } from '../services/event.details.service';
import { EventService } from '../services/event.service';

@Directive({
    selector: '[appNameExists]',
    providers: [
        {provide: NG_ASYNC_VALIDATORS, useExisting: NameExistsValidator, multi: true}
    ]
  })
  export class NameExistsValidator implements AsyncValidator {
    @Input() type: string;

    constructor(private tournamentService: TournamentService,
                private eventService: EventService, private eventDetailService: EventDetailsService) {}

    validate(control: AbstractControl): Observable<ValidationErrors | null> {
      const val = control.value;
      if (this.type === 'Tournament'){
        const obs = this.tournamentService.isNameValid(val).pipe(
          map((res) => {
              if (res) { return null; }
              else { return {duplicateName: 'Tournament name already taken'}; }
            })
          );
        return obs;
      }

      if (this.type === 'Event'){
        const obs = this.eventService.isNameValid(val).pipe(
          map((res) => {
              if (res) { return null; }
              else { return {duplicateName: 'Event name already taken'}; }
            })
          );
        return obs;
      }

      if (this.type === 'EventDetail'){
        const obs = this.eventDetailService.isNameValid(val).pipe(
          map((res) => {
              if (res) { return null; }
              else { return {duplicateName: 'Event detail name already taken'}; }
            })
          );
        return obs;
      }

      return null;
    }
  }