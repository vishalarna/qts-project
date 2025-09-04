import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class SweetAlertService {
  swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger',
    },
    buttonsStyling: false,
  });

  confirmAlert(confirmMessage: string) {
    return this.swalWithBootstrapButtons.fire({
      title: 'Are you sure?',
      text: confirmMessage,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Yes!',
      cancelButtonText: 'No!',
      reverseButtons: true,
    });
  }
  successAlert(title: string, description?: string) {
    Swal.fire({
      title: title,
      text: description,
      icon: 'success',
    });
  }

  errorAlert(title: string, description?: string) {

    Swal.fire({
      title: title,
      text: description,
      icon: 'error',
    });
  }

  errorAlertRedirect(title: string, description?: string) {
    return this.swalWithBootstrapButtons.fire({
      title: title,
      text: description,
      icon: 'error',
      showCancelButton: false,
      confirmButtonText: 'Ok',
      reverseButtons: true,
    });
  }

  warningAlert(title: string, description?: string) {
    Swal.fire({
      title: title,
      text: description,
      icon: 'warning',
    });
  }

  successToast(title: string,isEMP:boolean = false) {
    let Toast = Swal.mixin({
      toast: true,
      position: 'bottom-right',
      background: isEMP === false ? 'rgb(92, 155, 49)':'rgb(69, 111, 165)',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
      didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer);
        toast.addEventListener('mouseleave', Swal.resumeTimer);
      },
    });
    Toast.fire({
      title: "<span style='color:#fff'>" + title + '</span>',
      icon: 'success',
      iconColor:isEMP === false ? null:'rgb(255, 255, 255)',
    });
  }
  notificationSuccessToast(title: string) {
    let Toast = Swal.mixin({
      toast: true,
      position: 'top-right',
      background: 'rgb(92, 155, 49)',
      showConfirmButton: false,
      timer: 6000,
      timerProgressBar: true,
      didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer);
        toast.addEventListener('mouseleave', Swal.resumeTimer);
      },
    });
    Toast.fire({
      title: "<span style='color:#fff'>" + title + '</span>',
      icon: 'success',
    });
  }

  errorToast(title: string) {
    let Toast = Swal.mixin({
      toast: true,
      position: 'bottom-right',

      background: '#e74c3c',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
      didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer);
        toast.addEventListener('mouseleave', Swal.resumeTimer);
      },
    });
    Toast.fire({
      title: "<span style='color:#fff'>" + title + '</span>',
      icon: 'error',
    });
  }

  warningToast(title: string) {
    let Toast = Swal.mixin({
      toast: true,
      position: 'bottom-right',

      background: '#f1c40f',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
      didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer);
        toast.addEventListener('mouseleave', Swal.resumeTimer);
      },
    });
    Toast.fire({
      title: "<span style='color:#fff'>" + title + '</span>',
      icon: 'warning',
    });
  }
}
