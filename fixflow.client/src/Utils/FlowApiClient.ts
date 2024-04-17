import axios, { AxiosInstance, AxiosResponse } from "axios";

import { paths, components } from "../../../FixFlow.Server/swagger.json";

// Define your API client class
export class FlowApiClient {
  function(): void {
    const x = components.schemas.ClientDTO;
  }

  private httpClient: AxiosInstance;

  constructor(baseURL: string) {
    this.httpClient = axios.create({
      baseURL,
    });
  }
}
