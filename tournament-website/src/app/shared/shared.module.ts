import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NameExistsValidator } from './validators/nameExists.validator';

@NgModule({
    declarations: [
      // Validators
      NameExistsValidator
    ],
    imports: [
      CommonModule,
      RouterModule,
      FormsModule,
      ReactiveFormsModule,
      NgbModule
    ],
    exports: [
      // Modules
      NgbModule,
      FormsModule,
      ReactiveFormsModule,

      // Validators
      NameExistsValidator
    ],
  })
  export class SharedModule {}
