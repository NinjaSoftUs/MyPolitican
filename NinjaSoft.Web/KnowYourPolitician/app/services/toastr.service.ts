import { Injectable } from '@angular/core';

declare let toastr:any

@Injectable()
export class ToastrService {

constructor() { }

sucess(msg:string,title?:string){toastr.sucess(msg,title);}
warning(msg:string,title?:string){toastr.waring(msg,title);}
info(msg:string,title?:string){toastr.info(msg,title);}
error(msg:string,title?:string){toastr.error(msg,title);}

}