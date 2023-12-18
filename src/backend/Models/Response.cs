using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Models;

public class Response<T> {
    string Errror;
    T Data;

    public static Response<T> Ok(dynamic data) {
        var response = new Response<T> {
            Data = data
        };

        return response;
    }

    public static Response<T> Error(string error) {
        var response = new Response<T> {
            Errror = error
        };

        return response;
    }
}