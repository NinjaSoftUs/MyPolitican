import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs/RX';
import { IPolitician } from './politician.model';
import { Http, Response, Headers, RequestOptions } from '@angular/http';


@Injectable()
export class AppService {
    constructor(private http: Http) { }

    getImgList(theType: String): Observable<string[]> {
        return this.http.get("api/imglist/" + theType)
            .map(
            (response: Response) => {
                return <string[]>response.json();
            }).catch(this.handelError);
    }

    upload(fileToUpload: any, theType: string) {
        let input = new FormData();
        input.append("file", fileToUpload);

        let url = "/api/uploadImg/" + theType;
        return this.http
            .post(url, input);
    }

    getPoliticianById(id: number) { }
    getSponserById(id: number) { }
    careateNewSponser(sponser: any) {
        return sponser;
    }
    deltePoliticanById(id: number) {
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let options = new RequestOptions({ headers: headers });
        let url = '/api/deletePolitican/{id}';
        this.http.delete(url).catch(this.handelError).subscribe();
    }

    careateNewPolitician(politician: IPolitician): Observable<IPolitician> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post('/api/events', JSON.stringify(politician), options).map((response: Response) => {
            return response.json();
        }).catch(this.handelError);
    }
    searchPolitions(searchTerm: string) {
        return this.http.get("/api/politicans/search?search=" + searchTerm).map((response: Response) => {
            return response.json();
        }).catch(this.handelError);
    }

    getPoliticians(): Observable<IPolitician[]> {
        return this.http.get("api/politicans").map((response: Response) => {
            return <IPolitician[]>response.json();
        }).catch(this.handelError);
    }

    private handelError(error: Response) {
        console.log(error);
        return Observable.throw(error.statusText);
    }

    upCertPolitican(formValues) {
        console.log("upCertPolitican called");
        console.log(formValues);
        let headers = new Headers({ 'Content-Type': 'image/png' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post('api/upCertPolitican', JSON.stringify(formValues), options).map((response: Response) => {
            return response.json();
        }).catch(this.handelError).subscribe();
    }

    //upLoadImg(img) {
    //    console.log("upload img called");
    //    console.log(img);
    //    let headers = new Headers({ 'Content-Type': 'application/json',  'enctype':'multipart/form-data' });
    //    let options = new RequestOptions({ headers: headers });

    //    return this.http.post('api/uploadImg', img, options).map((response: Response) => {
    //       return response.json();
    //    }).catch(this.handelError).subscribe();
    //}
}