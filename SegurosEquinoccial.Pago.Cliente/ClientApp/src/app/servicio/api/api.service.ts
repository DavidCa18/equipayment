import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { Globales } from "../../variables/globales/globales.service";
import { retry } from "rxjs/operators";

@Injectable()
export class ApiService extends Globales {

  public url: string = "";
  constructor(private http: HttpClient) {
    super();
    this.url = this.obtenerCredenciales().conexionAPI;
  }

  post(endpoint: string, body: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        "Authorization": "A1TLHztUKEaK/RaqKqBRsM9xlGFEoiAQ"
      })
    };
    return this.http.post(this.url + "" + endpoint, body, httpOptions).pipe(retry(3));;
  }

  get(endpoint: string, params?: any, reqOpts?: any): Observable<any> {
    if (!reqOpts) {
      reqOpts = {
        params: new HttpParams()
      };
    }

    if (params) {
      reqOpts.params = new HttpParams();
      for (let k in params) {
        reqOpts.params.set(k, params[k]);
      }
    }

    reqOpts = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        "Authorization": "A1TLHztUKEaK/RaqKqBRsM9xlGFEoiAQ"
      })
    };

    return this.http.get(this.url + "" + endpoint, reqOpts).pipe(retry(3));
  }

  getIP(endpoint: string): Observable<any> {
    return this.http.get(this.obtenerCredenciales().conexionIP + "" + endpoint).pipe(retry(3));;
  }

  error(clase, metodo, uriTemplate, estatus, url, descripcion, idUsuario, nombreUsuario) {
    var Error_ = {
      Identificador: 1,
      Clase: clase,
      Metodo: metodo,
      UriTemplate: uriTemplate,
      Estatus: estatus,
      Url: url,
      Descripcion: descripcion,
      IdUsuario: idUsuario,
      NombreUsuario: nombreUsuario
    };

    this.post("Gestion/SGesGestion.svc/pago/error/gestion", Error_).subscribe(
      (res: any) => {
        console.log("Error Guardado Exitosamente");
      },
      err => {
        console.log(err);
      }
    );
  }

}
