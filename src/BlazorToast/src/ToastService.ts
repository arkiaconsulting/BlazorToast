interface IToastService {
  IsSupported(): boolean;
  RequestPermission(): Promise<string>;
  Create(title: string, options: object): object;
}

class ToastService implements IToastService {
  public RequestPermission = (): Promise<string> => {
    return new Promise((resolve, reject) => {
      Notification.requestPermission((permission) => {
        resolve(permission);
      });
    });
  }

  public IsSupported = (): boolean => {
    if (("Notification" in window))
      return true;
    return false;
  }

  public Create = (title: string, options: object): object => {
    var note = new Notification(title, options);
    return note;
  }
}

let toastService: ToastService = new ToastService();

export function isSupported(): boolean {
    return toastService.IsSupported();
}