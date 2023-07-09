import { throwError } from "rxjs";


export class Global {

    public static Anwendungsnamen= 'Creepy Client';


    public static handleError(error: any) {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
          // Get client-side error
          errorMessage = error.error.message;
        } else {
          // Get server-side error
          errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        console.error(errorMessage);
        return throwError(() => {
          return errorMessage;
        });
      }
    


}
