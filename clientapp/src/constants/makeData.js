const range = len => {
    const arr = []
    for (let i = 0; i < len; i++) {
        arr.push(i)
    }
    return arr
}

const newItem = () => {
    return {
        id: "945f1ee6-76cd-48af-9e74-7d6004346583",
        status: "Требуется анализ",
        isSystemScanClean: false,
        fileName: "945f1ee6-76cd-48af-9e74-7d6004346583.png",
        typeName: "File",
        md5: "26e837171df4af865a6eafb353ab741d",
        sha1: "1b8068e7044546ec57f088b44e6bc24f949fe0ca",
        sha256: "71271468f0eeb0c2c8053727140faf7287dea0f236293864cc280ed342f70419",
        priority: "Low",
        ipAddrees: "",
        domain: null,
        code: "",
        resource: "71271468f0eeb0c2c8053727140faf7287dea0f236293864cc280ed342f70419"
    }
}

export default function makeData(...lens) {
    const makeDataLevel = (depth = 0) => {
        const len = lens[depth]
        return range(len).map(d => {
            return {
                ...newItem(),
                subRows: lens[depth + 1] ? makeDataLevel(depth + 1) : undefined,
            }
        })
    }

    return makeDataLevel()
}