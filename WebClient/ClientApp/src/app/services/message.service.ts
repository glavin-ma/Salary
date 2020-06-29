import { Injectable } from "@angular/core";
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class MessageService {
  constructor(private toastr: ToastrService) { }


  public showError(message: string, header: string = 'Error !!!'): void {
    this.toastr.error(message, header);
  }

  public showSuccess(message: string): void {
    this.toastr.success(message, 'Success.');
  }

}
