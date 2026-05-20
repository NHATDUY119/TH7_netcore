const API_BASE_URL = (window.location.origin && window.location.origin !== 'null')
    ? `${window.location.origin}/api`
    : 'http://localhost:5135/api';

// ==================== Hàng Hóa APIs ====================

async function getHangHoas() {
    try {
        const response = await fetch(`${API_BASE_URL}/hanghoa`);
        if (!response.ok) throw new Error('Lỗi khi lấy dữ liệu');
        return await response.json();
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

async function getHangHoaById(id) {
    try {
        const response = await fetch(`${API_BASE_URL}/hanghoa/${id}`);
        if (!response.ok) throw new Error('Lỗi khi lấy dữ liệu');
        return await response.json();
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

async function searchHangHoas(tenHang, donGiaMin, donGiaMax) {
    try {
        const params = new URLSearchParams();
        if (tenHang) params.append('tenHang', tenHang);
        if (donGiaMin) params.append('donGiaMin', donGiaMin);
        if (donGiaMax) params.append('donGiaMax', donGiaMax);

        const response = await fetch(`${API_BASE_URL}/hanghoa/search/filter?${params.toString()}`);
        if (!response.ok) throw new Error('Lỗi khi tìm kiếm');
        return await response.json();
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

async function addHangHoa(data) {
    try {
        const response = await fetch(`${API_BASE_URL}/hanghoa`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();
        if (!response.ok) {
            throw new Error(result.message || 'Lỗi khi thêm hàng hóa');
        }
        return result;
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

async function updateHangHoa(id, data) {
    try {
        const response = await fetch(`${API_BASE_URL}/hanghoa/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();
        if (!response.ok) {
            throw new Error(result.message || 'Lỗi khi cập nhật hàng hóa');
        }
        return result;
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

async function deleteHangHoa(id) {
    try {
        const response = await fetch(`${API_BASE_URL}/hanghoa/${id}`, {
            method: 'DELETE'
        });

        const result = await response.json();
        if (!response.ok) {
            throw new Error(result.message || 'Lỗi khi xóa hàng hóa');
        }
        return result;
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

// ==================== Nhà Cung Cấp APIs ====================

async function getNhaCungCaps() {
    try {
        const response = await fetch(`${API_BASE_URL}/nhacungcap`);
        if (!response.ok) throw new Error('Lỗi khi lấy dữ liệu');
        return await response.json();
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

async function getNhaCungCapById(id) {
    try {
        const response = await fetch(`${API_BASE_URL}/nhacungcap/${id}`);
        if (!response.ok) throw new Error('Lỗi khi lấy dữ liệu');
        return await response.json();
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

async function addNhaCungCap(data) {
    try {
        const response = await fetch(`${API_BASE_URL}/nhacungcap`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();
        if (!response.ok) {
            throw new Error(result.message || 'Lỗi khi thêm nhà cung cấp');
        }
        return result;
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

async function updateNhaCungCap(id, data) {
    try {
        const response = await fetch(`${API_BASE_URL}/nhacungcap/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();
        if (!response.ok) {
            throw new Error(result.message || 'Lỗi khi cập nhật nhà cung cấp');
        }
        return result;
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

async function deleteNhaCungCap(id) {
    try {
        const response = await fetch(`${API_BASE_URL}/nhacungcap/${id}`, {
            method: 'DELETE'
        });

        const result = await response.json();
        if (!response.ok) {
            throw new Error(result.message || 'Lỗi khi xóa nhà cung cấp');
        }
        return result;
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}
