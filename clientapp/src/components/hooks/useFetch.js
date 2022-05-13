import { useState, useEffect } from "react";

export const useFetch = (uri) => {
    const [data, setData] = useState();
    const [error, setError] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetch(uri)
            .then(response => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .cath(setError)
    }, [])

    return {
        loading,
        data,
        error
    };
}