import { Injectable } from "@angular/core";
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class MessageService {
  constructor(private toastr: ToastrService) { }


  public showError(message: string): void {
    this.toastr.error(message, 'Error !!!');
  }

  public showSuccess(message: string): void {
    this.toastr.error(message, 'Success.');
  }

}
