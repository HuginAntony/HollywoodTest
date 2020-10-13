import { AbstractControl, AsyncValidator, NG_ASYNC_VALIDATORS, ValidationErrors, Validator} from '@angular/forms';
import {Directive} from '@angular/core';
import {Observable, of} from 'rxjs';
import { map } from 'rxjs/operators';
import { TournamentService } from '../services/tournament.service';

@Directive({
    selector: '[appTournamentExists]',
    providers: [
        {provide: NG_ASYNC_VALIDATORS, useExisting: TournamentExistsValidator, multi: true}
    ]
  })
  export class TournamentExistsValidator implements AsyncValidator {
 
    constructor(private authService: TournamentService) {}

    validate(control: AbstractControl): Observable<ValidationErrors | null> {
      const val = control.value;
 
      const obs = this.authService.get(val).pipe(
        map((res) => {
            if (!res) { return null; }
            else { return {emailTaken: 'Email address already taken'}; }
          })
        );
      return obs;
    }
  }