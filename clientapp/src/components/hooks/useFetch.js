import { useState, useEffect } from "react";

export function useFetch(uri) {
    const [data, setData] = useState();
    const [error, setError] = useState();
    const [loading, setLoading] = useState(true);

    console.log(uri);

    useEffect(() => {
        if (!uri) return;
        fetch(uri, {
            url: '/',
        })
            .then(response => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .cath(setError)
    }, [uri])

    return {
        loading,
        data,
        error
    };
}